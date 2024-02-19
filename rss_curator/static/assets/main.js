document.addEventListener("DOMContentLoaded", function () {
    const urlParams = new URLSearchParams(window.location.search);
    const configParam = urlParams.get('cfg');

    if (!configParam) {
        document.getElementById('panel_channels').classList.remove('hidden');
        getChannels()
            .then(channels => updateChannelsTable(channels))
    } else {
        const channelId = configParam;
        document.getElementById('panel_settings').classList.remove('hidden');
        getChannel(channelId)
            .then(channel => updateTitlesTable(channel))
    }
});

function getChannels() { return fetch('../channels').then(r => r.json()); }
function getChannel(channelId) { return fetch(`../channels/${channelId}`).then(r => r.json()); }
function patchChannelTitle(channelId, titleId, followed) {
    followed = followed ? 1 : 0;
    return fetch(`../channels/${channelId}/items/${titleId}?followed=${followed}`, { method: 'PATCH' })
        .then(r => r.json());
}

function updateChannelsTable(channels) {
    const containerNode = document.getElementById('table_channels')
    const templateNode = document.getElementById('_template_channelrow')

    const pathname = window.location.pathname;  // like /foo/bar/index.html
    const path = pathname.substring(0, pathname.lastIndexOf('/')) + '/';

    updateList(channels, containerNode, templateNode, "No channels", function (node, channel) {
        const nameNode = node.querySelector('[id=name_label]');
        const rssNode = node.querySelector('[id=rss_href]');
        const cfgNode = node.querySelector('[id=cfg_href]');

        set_value(nameNode, 'innerHTML', channel.title);
        set_value(rssNode, 'href', `${path}${channel.relativeUrl}`);
        set_value(cfgNode, 'href', `${pathname}?cfg=${channel.id}`);
    });
}

function updateTitlesTable(channel) {
    const panelTitleNode = document.getElementById('panel_settings_title');
    const containerNode = document.getElementById('table_titles');
    const templateNode = document.getElementById('_template_titlerow');

    const channelId = channel.id;

    panelTitleNode.innerHTML = channel.title;

    updateList(channel.items, containerNode, templateNode, "No title", function (node, title) {
        const labelNode = node.querySelector('[id=title_label]');
        const checkboxNode = node.querySelector('[id=switch_input]');

        set_value(labelNode, 'innerHTML', title.title)
        set_value(checkboxNode, 'checked', title.followed);
        set_value(checkboxNode.dataset, 'channel_id', channelId);
        set_value(checkboxNode.dataset, 'title_id', title.id);
    });
}

function onTitleSwitchClick(checkbox) {
    const channelId = checkbox.dataset.channel_id;
    const titleId = checkbox.dataset.title_id;

    patchChannelTitle(channelId, titleId, checkbox.checked)
        .then(channel => updateTitlesTable(channel))
}

function updateList(source, containerNode, templateNode, emptyPlaceholderLabel, nodeUpdater) {
    // remove placeholder
    let placeholderNode = containerNode.querySelector('[id=_placeholder_text]');
    const lenFix = Array.from(containerNode.childNodes).filter(n => n.nodeType === Node.TEXT_NODE || n.nodeType === Node.COMMENT_NODE).length
        + (placeholderNode ? 1 : 0);

    // add missing template
    while (containerNode.childNodes.length - lenFix < source.length) {
        let child = templateNode.cloneNode(true);
        child.dataset.rowindex = containerNode.childNodes.length - lenFix;
        containerNode.appendChild(child)
    }

    // remove excessive template
    while (containerNode.childNodes.length - lenFix > source.length) {
        containerNode.childNodes[containerNode.childNodes.length - 1].remove()
        console.assert(containerNode.dataset.rowindex == containerNode.childNodes.length - lenFix)
    }

    // apply rendering
    for (let i = 0; i < containerNode.childNodes.length - lenFix; i++) {
        nodeUpdater(containerNode.childNodes[i + lenFix], source[i]);
    }

    // add placeholder when no element
    if (placeholderNode) {
        if (source.length <= 0 && emptyPlaceholderLabel) {
            // setup 'placehoder'
            placeholderNode.innerHTML = emptyPlaceholderLabel
            placeholderNode.classList.remove('hidden');
        } else {
            // remove placeholder
            placeholderNode.innerHTML = ''
            if (!placeholderNode.classList.contains('hidden'))
                placeholderNode.classList.add('hidden')
        }
    }
}

function set_value(obj, name, value) {
    if (obj[name] === value)
        return;
    obj[name] = value;
}