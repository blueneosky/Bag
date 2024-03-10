class ApiService {
    constructor(root) {
        this._root = root
    }

    getChannels() {
        return this.securedFetch('channels').then(r => r.json());
    }

    getChannel(channelId) {
        return this.securedFetch(`channels/${channelId}`).then(r => r.json());
    }

    patchChannelTitle(channelId, titleId, followed) {
        followed = followed ? 1 : 0;
        return this.securedFetch(`channels/${channelId}/items/${titleId}?followed=${followed}`, { method: 'PATCH' })
            .then(r => r.json());
    }

    securedFetch(input, init) {
        return fetch(`${this._root}/${input}`, init)
            .then(r => {
                if (r.status === 401 || r.status === 403) {
                    // TODO
                    console.info('forbiden')
                    return undefined
                }
                return r;
            })
    }
}

export const apiService = new ApiService('..');