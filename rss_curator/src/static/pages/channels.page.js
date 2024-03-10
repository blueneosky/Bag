import { apiService } from "../modules/api.service.js";
import { DHList, set_value } from "../modules/dhtml.js";

export class ChannelsPage {
    #location_pathname; /* String */
    #location_path; /* String */

    #panelChannels = new DHList(
        "table_channels",
        "_template_channels_row",
        {
            itemUpdater: (node, channel) => this.__tableChannelsItemUpdater(node, channel),
            placeholder: "_template_channels_placeholder",
        });

    constructor() {
        this.#location_pathname = window.location.pathname;  // like /foo/bar/index.html
        this.#location_path = this.#location_pathname.substring(0, this.#location_pathname.lastIndexOf('/')) + '/';
    }

    show() {
        document.getElementById('panel_channels').classList.remove('hidden');

        apiService.getChannels()
            .then(channels => this.#panelChannels.updateList(channels));
    }

    __tableChannelsItemUpdater(node, channel) {
        const nameNode = node.querySelector('[id=name_label]');
        const rssNode = node.querySelector('[id=rss_href]');
        const cfgNode = node.querySelector('[id=cfg_href]');

        set_value(nameNode, 'innerHTML', channel.title);
        set_value(rssNode, 'href', `${this.#location_path}../${channel.relativeUrl}`);
        set_value(cfgNode, 'href', `${this.#location_pathname}?cfg=${channel.id}`);
    }
}