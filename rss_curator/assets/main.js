document.addEventListener("DOMContentLoaded", function () {
    const urlParams = new URLSearchParams(window.location.search);
    const configParam = urlParams.get('cfg');

    if (!configParam) {
        document.getElementById('panel_channels').classList.remove('hidden');
        fetch('./channels')
            .then(r => r.json())
            .then(channels => updateChannelsTable(channels))
    } else {
        document.getElementById('panel_settings').classList.remove('hidden');
        fetch(`./channels/${configParam}`)
            .then(r => r.json())
            .then(channels => updateTitlesTable(channels))
    }
});

function updateChannelsTable(channels) {
    const containerNode = document.getElementById('table_channels')
    const templateNode = document.getElementById('_template_channelrow')

    const pathname = window.location.pathname;  // like /foo/bar/index.html
    const path = pathname.substring(0, pathname.lastIndexOf('/')) + '/';

    update(channels, containerNode, templateNode, "No channels", function (node, channel) {
        const nameNode = node.querySelector('[id=name_label]');
        const rssNode = node.querySelector('[id=rss_href]');
        const cfgNode = node.querySelector('[id=cfg_href]');

        nameNode.innerHTML = channel.title;
        rssNode.setAttribute('href', `${path}${channel.relativeUrl}`)
        cfgNode.setAttribute('href', `${pathname}?cfg=${channel.id}`)
    });
}

function updateTitlesTable(titles) {
    const containerNode = document.getElementById('table_titles')
    const templateNode = document.getElementById('_template_titlerow')

    update(titles, containerNode, templateNode, "No title", function (node, title) {
        const labelNode = node.querySelector('[id=title_label]');
        const checkboxNode = node.querySelector('[id=switch_input]');

        labelNode.innerHTML = title.title;
        checkboxNode.checked = title.followed;
    });
}

function update(source, containerNode, templateNode, emptyPlaceholderLabel, nodeUpdater) {
    // remove placeholder
    let placeholderNode = containerNode.querySelector('[id=_placeholder_text]');
    const lenFix = Array.from(containerNode.childNodes).filter(n => n.nodeType === Node.TEXT_NODE || n.nodeType === Node.COMMENT_NODE).length
        + (placeholderNode ? 1 : 0);

    // add missing template
    while (containerNode.childNodes.length - lenFix < source.length) {
        let child = templateNode.cloneNode(true);
        child.setAttribute('id', `__child_${containerNode.childNodes.length - lenFix}`);
        containerNode.appendChild(child)
    }

    // remove excessive template
    while (containerNode.childNodes.length - lenFix > source.length) {
        containerNode.childNodes[containerNode.childNodes.length - 1].remove()
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
