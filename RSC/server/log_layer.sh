#!/bin/bash

source env.sh

#vim tokens: log_append log_append_login_status

# $1 : message
log_append() {
	echo "[$(date -Is)] $@" >> $LOG_FILE_PATH
}

# $1 : status (OK/KO)
log_append_login_status() {
	if [ "$1" == "OK" ]; then
		local LALS_STATUS="success"
	else
		local LALS_STATUS="failed"
	fi
	log_append "Login $LALS_STATUS; user [${USER_NAME}]; ${REMOTE_ADDR}"
}

