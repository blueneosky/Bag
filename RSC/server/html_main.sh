#!/bin/bash

source env.sh
source html_common.sh


html_print_main_page() {
    printf "<!DOCTYPE html>"
    printf "<html>"
	html_print_head_content
    printf "<body>"
	printf "<div class='main_div'>"
#	env | sed  's#$#<br/>#'
#	printf "HTTP_COOKIE=%s<br/>" "$HTTP_COOKIE"
	printf "COOKIE_SESSION_ID=%s<br/>" "$COOKIE_SESSION_ID"
    printf "	<h1>NouNours Station</h1>"
    printf "	<div class='actions_div'>"
    printf "	<table class='actions_table'>"
    printf "	<tbody>"
    printf "		<tr class='actions_table_tr'><td class='actions_table_td'>"
    printf "			<a href='%s?%s'    class='a_as_button'>Get ready !</a>" "$SCRIPT_NAME" "$ACTION_WAKEUP"
    printf "		</td></tr>"
    printf "		<tr class='actions_table_tr'><td class='actions_table_td'>"
    printf "			<a href='%s?%s' class='a_as_button'>Take a nap</a>" "$SCRIPT_NAME" "$ACTION_LONGSLEEP"
    printf "		</td></tr>"
    printf "	</tbody>"
    printf "	</table>"
    printf "	</div>"
    printf "</div>"
    printf "</body>"
	html_print_bottom_page_content
    printf "</html>"
}

