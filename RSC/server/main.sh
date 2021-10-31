#!/bin/bash

source html_common.sh
source html_main.sh
source html_login.sh
source html_redirect.sh
source data_layer.sh
source commands.sh
source user_authentication.sh


if [ "$REQUEST_METHOD" = "POST" ]; then
	process_login
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

