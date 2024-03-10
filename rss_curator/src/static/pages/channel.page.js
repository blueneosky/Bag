import { apiService } from "../modules/api.service.js";
import { DHList, set_value } from "../modules/dhtml.js";

export class ChannelPage {
    #channelId;

    #panelTitles = new DHList(
        "table_titles",
        "_template_titles_row",
        {
            itemUpdater: (node, title) => this.__tableTitlesItemUpdater(node, title),
            afterItemInit: (node) => this.__tableTitlesAfterItemInit(node),
            placeholder: "_template_titles_placeholder",
        });

    constructor(channelId) {
        this.#channelId = channelId;

    }

    show() {
        document.getElementById('panel_settings').classList.remove('hidden');
        apiService.getChannel(this.#channelId)
            .then(channel => {
                const panelTitleNode = document.getElementById('panel_settings_title');

                panelTitleNode.innerHTML = channel.title;
                this.#panelTitles.updateList(channel.items);
            });
    }

    __tableTitlesItemUpdater(node, title) {
        const labelNode = node.querySelector('[id=title_label]');
        const checkboxNode = node.querySelector('[id=switch_input]');

        set_value(labelNode, 'innerHTML', title.title)
        set_value(checkboxNode, 'checked', title.followed);
        set_value(checkboxNode.dataset, 'title_id', title.id);
    }

    __tableTitlesAfterItemInit(node) {
        const checkboxNode = node.querySelector('[id=switch_input]');
        checkboxNode.onclick = () => this.__onCheckboxClick(checkboxNode);
    }

    __onCheckboxClick(checkboxNode) {
        const titleId = checkboxNode.dataset.title_id;

        apiService.patchChannelTitle(this.#channelId, titleId, checkboxNode.checked)
            .then(channel => this.#panelTitles.updateList(channel.items))
    }
}