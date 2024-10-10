export function set_value(obj, name, value) {
    if (obj[name] === value)
        return;
    obj[name] = value;
}

export class DHList {
    static #DATA_ITEMS_COUNT_NAME = 'dhlist_items_count';
    static #DATA_ITEM_IDX_NAME = 'dhlist_item_idx';

    #containerNode;
    #itemTemplateNode;
    #placeholderTemplateNode;

    #itemUpdater;    // function(itemElement, itemData)=>void | undefined
    #afterItemInit;  // function(HTMLElement)=>void | undefined

    constructor(
        container,                  /* String | Element */
        template,                   /* String | Element */
        options,                    /* undefined | class {
                                            itemUpdater,      // function(itemElement, itemData)=>void | undefined
                                            afterItemInit,    // function(HTMLElement)=>void | undefined
                                            placeholder,      // String | Element | undefined
                                        } */
    ) {
        this.#containerNode = typeof container === 'string'
            ? document.getElementById(container)
            : container;
        this.#itemTemplateNode = typeof template === 'string'
            ? document.getElementById(template)
            : template;
        this.#placeholderTemplateNode = typeof options?.placeholder === 'string'
            ? document.getElementById(options.placeholder)
            : options?.placeholder;
        
        this.#itemUpdater = options?.itemUpdater ?? function () { };
        this.#afterItemInit = options?.afterItemInit ?? function () { };
    }

    updateList(data/*array*/) {
        this.#updateElementList(data.length);

        const containerNode = this.#containerNode;
        let itemElements = this.#containerNode.querySelectorAll("[data-" + DHList.#DATA_ITEM_IDX_NAME + "]")

        // apply rendering
        for (let i = 0; i < itemElements.length; i++) {
            this.#itemUpdater(itemElements[i], data[i]);
        }
    }

    #updateElementList(dataLength) {
        const containerNode = this.#containerNode;
        let itemNodesCount = this.#containerNode.dataset[DHList.#DATA_ITEMS_COUNT_NAME];

        // init
        if (itemNodesCount === undefined) {
            itemNodesCount = 0;
            // remove 'dev' lefthover
            this.#containerNode.innerHTML = ''
        }

        if (itemNodesCount === 0 && dataLength > 0) {
            // remove 'placeholder'
            this.#containerNode.innerHTML = ''
        }

        // add missing template
        while (itemNodesCount < dataLength) {
            const itemIdx = itemNodesCount;
            itemNodesCount++;
            let childNode = this.#itemTemplateNode.cloneNode(true);
            childNode.id += ':' + itemIdx;
            childNode.dataset[DHList.#DATA_ITEM_IDX_NAME] = itemIdx;
            containerNode.appendChild(childNode);
            this.#afterItemInit(childNode);
        }

        // remove excessive template
        while (itemNodesCount > dataLength) {
            itemNodesCount--;
            const itemIdx = itemNodesCount;
            let childNode = containerNode.querySelector("[data-" + DHList.#DATA_ITEM_IDX_NAME + "='" + itemIdx + "']");
            childNode.remove();
        }

        containerNode.dataset[DHList.#DATA_ITEMS_COUNT_NAME] = itemNodesCount;

        // placeholder
        if (itemNodesCount ===0 && this.#placeholderTemplateNode) {
            let childNode = this.#placeholderTemplateNode.cloneNode(true);
            childNode.id += ':_';
            containerNode.appendChild(childNode)
        }
    }
}