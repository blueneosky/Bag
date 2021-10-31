#!/bin/bash

source env.sh
source html_common.sh


# $1 : uri to redirect
html_print_redirect_page() {
	printf "<!DOCTYPE html>"
	printf "<meta http-equiv='refresh' content='0; url=%s' />" $1
}


# $1 : uri to redirect
# $2 : duration (in sec)
html_print_delayed_redirect_page() {
	printf "<!DOCTYPE html>"
    printf "<html>"
	html_print_head_content	\
		$(printf "	<meta http-equiv='refresh' content='%i; url=%s' />" "$2" "$1")
    printf "<body>"
	printf "<div class='main_div'>"
    printf "	<h1>Loading...</h1>"
	printf "	<img src='%s' alt='Loading...' class='loading_img' />" "$AWAIT_IMG_URI"
    printf "</div>"
	html_print_bottom_page_content
    printf "</body>"
    printf "</html>"
}

