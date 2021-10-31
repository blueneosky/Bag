#!/bin/bash

source env.sh


# $@ : any arguments will be added before the end of </head>
html_print_head_content() {
    printf "<head>"
    printf "	<meta charset='utf-8' />"
    printf "	<meta name='viewport' content='width=device-width, initial-scale=1'>"
    printf "	<link rel='stylesheet' href='%s' />" "$CSS_URI"
	printf "	<link rel='icon' href='%s' type='image/x-icon' />" "$FAVICON_URI"
    printf "	<title>%s Station overview</title>" "$TITLE_NAME"
	printf "	%s" "$@"
    printf "</head>"
}

html_print_bottom_page_content() {
    printf "<div class='bottom_div'>"
	printf "	<i>Cookies will be used for authentication only</i><br/>"
    printf "	%s" "$SERVER_SIGNATURE"
	printf "</div>"
}

