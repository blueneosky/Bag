#!/bin/bash

source web_layer.sh
source html_common.sh
source html_main.sh
source html_login.sh
source html_redirect.sh
source data_layer.sh
source commands.sh
source user_authentication.sh

# $1 : user name
# $2 : pass
# $3 : remenberme
process_login() {

	#check user
	if ! user_is_valid "$1" "$2"; then
		#nop - user not recognized or bad password (don't want to gove an hint)
		log_append_login_status "KO"

		local PL_SCRIPT_NAME=$(echo "$SCRIPT_NAME" | sed -e 's/\?.*$//')

		http_print_header
		html_print_redirect_page "${PL_SCRIPT_NAME}?nok=loginfailed"

		exit 0
	fi

	#good - login success
	log_append_login_status "OK"

	local PL_TOKEN=$(uuidgen)
	local PL_EXPIRATION_DATE=#TODO
	user_update_token "$1" "$PL_TOKEN" "$PL_EXPIRATION_DATE"

#TODO make a redirect with cookies
	http_print_header
    printf "<!DOCTYPE html>"
    printf "<html>"
	html_print_head_content
    printf "<body>"
	printf "GOOD !"
#	env | sed -e 's#$#<br/>#g'
	exit 0
}


if [ "$REQUEST_METHOD" = "POST" ]; then
	# extract data from post content
	POST_DATA=$(    http_get_post_data)
	USER_NAME=$(    http_data_get_get_value "$POST_DATA" "$LOGIN_NAME")
	USER_PASSWORD=$(http_data_get_get_value "$POST_DATA" "$LOGIN_PASSWORD")
	USER_REMEMBER=$(http_data_get_get_value "$POST_DATA" "$LOGIN_REMEMBER")

	process_login "$USER_NAME" "$USER_PASSWORD" "$USER_REMEMBER" "$POST_DATA"
	exit 0
fi

local COOKIE_SESSION_ID=$(http_cookie_get_value "$COOKIE_SESSION_ID_NAME")
if [ -z "$COOKIE_SESSION_ID" ]; then
	http_print_header
	html_print_login_page
	exit 0
fi

http_print_header


if [ "$QUERY_STRING" = "$ACTION_WAKEUP" ]; then
	send_magic_packet
	html_print_delayed_redirect_page "$SCRIPT_NAME" 3
	exit 0
fi

if [ "$QUERY_STRING" = "$ACTION_LONGSLEEP" ]; then
	send_longsleep_packet
	html_print_delayed_redirect_page "$SCRIPT_NAME" 3
	exit 0
fi

html_print_main_page
exit 0

