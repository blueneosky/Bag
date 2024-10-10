import { apiService } from "../modules/api.service.js";
import { DHList, set_value } from "../modules/dhtml.js";

export class ServicesPage {
    #location_pathname; /* String */
    #location_path; /* String */

    #panelServices = new DHList(
        "table_services",
        "_template_services_row",
        {
            itemUpdater: (node, channel) => this.__tableChannelsItemUpdater(node, channel),
            placeholder: "_template_services_placeholder",
        });

    constructor() {
        this.#location_pathname = window.location.pathname;  // like /foo/bar/index.html
        this.#location_path = this.#location_pathname.substring(0, this.#location_pathname.lastIndexOf('/')) + '/';

        setInterval(() => this.#updateList(), 5000);
    }

    show() {
        document.getElementById('pannel_services').classList.remove('hidden');
        this.#updateList();
    }

    #updateList() {
        apiService.getServices()
            .then(services => this.#panelServices.updateList(services));
    }

    __tableChannelsItemUpdater(node, service) {
        const statusDiv = node.querySelector('[id=status_div]');
        const nameTh = node.querySelector('[id=name_label]');
        const ssHref = node.querySelector('[id=start_stop_href]');
        // const ssImg = node.querySelector('[id=start_stop_img]');
        const rsHref = node.querySelector('[id=restart_href]');

        // let ssSrcIcon = service.status == 'STOPPED' ? 'start.svg' : 'stop.svg';
        let ssEnable = ['STARTED', 'STOPPED'].includes(service.status);
        let rsEnable = service.status == 'STARTED';

        set_value(node.dataset, 'service_id', service.id);
        set_value(node.dataset, 'service_status', service.status);
        set_value(statusDiv.dataset, 'value', service.status);
        set_value(nameTh, 'innerHTML', service.name);
        set_value(ssHref.dataset, 'enable', ssEnable);
        // set_value(ssImg, 'src', `assets/${ssSrcIcon}`);
        set_value(rsHref.dataset, 'enable', rsEnable);

        if (!ssHref.onclick) ssHref.onclick = () => {
            const id = node.dataset.service_id;
            const status = node.dataset.service_status;
            if (status == 'STARTED') {
                apiService.stopService(id);
            } else if (status == 'STOPPED') {
                apiService.startService(id);
            }
            return false;
        };

        if (!rsHref.onclick) rsHref.onclick = () => {
            const id = node.dataset.service_id;
            const status = node.dataset.service_status;
            if (status == 'STARTED')
                apiService.restartService(id);
            return false;
        };
    }
}