class ApiService {
    constructor(root) {
        this._root = root
    }

    getServices() {
        return this.securedFetch('services').then(r => r.json());
    }

    getService(serviceId) {
        return this.securedFetch(`services/${serviceId}`).then(r => r.json());
    }

    startService(serviceId) {
        return this.securedFetch(`services/${serviceId}/start`).then(r => r.json());
    }

    startService(serviceId)   { return this.#postServiceCommand(serviceId, 'start'); }
    stopService(serviceId)    { return this.#postServiceCommand(serviceId, 'stop'); }
    restartService(serviceId) { return this.#postServiceCommand(serviceId, 'restart'); }

    #postServiceCommand(serviceId, command) {
        return this.securedFetch(`services/${serviceId}/${command}`, { method: 'POST' })
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