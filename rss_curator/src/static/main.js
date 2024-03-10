import { ChannelPage } from "./pages/channel.page.js";
import { ChannelsPage } from "./pages/channels.page.js";

const urlParams = new URLSearchParams(window.location.search);
const configParam = urlParams.get('cfg');

if (!configParam) {
    new ChannelsPage().show();
} else {
    new ChannelPage(configParam).show();
}

