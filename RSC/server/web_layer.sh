#!/bin/bash

# $1 : cookie name
get_cookie_value() {
	echo "$HTTP_COOKIE" | sed 's/; /\n/g' | grep "$1=" | sed 's/^.*=//'
}

print_header() {
	printf "Content-type: text/html\n"
	local COOKIE_SESSION_EXPIRATION_DATE=$(date -d "+1 years" +'%a, %d %b %Y %T GMT')	#for 1 year, we don't care to adjust time zone ;-)
#	printf "Set-Cookie: dummy1=lol\n"
#	printf "Set-Cookie: %s=none; HttpOnly; Secure; SameSite=Strict; Expires=%s'\n" "$COOKIE_SESSION_ID_NAME" "$COOKIE_SESSION_EXPIRATION_DATE"
#	printf "Set-Cookie: dummy2=lol\n"
	printf "\n"
}

# $1 : uri to redirect
show_redirect() {
	printf "<!DOCTYPE html>"
	printf "<meta http-equiv='refresh' content='0; url=%s' />" $1
}

# $@ : any arguments will be added before the end of </head>
print_html_head() {
    printf "<head>"
    printf "	<meta charset='utf-8' />"
    printf "	<meta name='viewport' content='width=device-width, initial-scale=1'>"
    printf "	<link rel='stylesheet' href='%s' />" "$CSS_URI"
	printf "	<link rel='icon' href='%s' type='image/x-icon' />" "$FAVICON_URI"
    printf "	<title>%s Station overview</title>" "$TITLE_NAME"
	printf "	%s" "$@"
    printf "</head>"
}

print_html_bottom_page_content() {
    printf "<div class='bottom_div'>"
	printf "	<i>Cookies will be used for authentication</i><br/>"
    printf "	%s" "$SERVER_SIGNATURE"
	printf "</div>"
}

# $1 : uri to redirect
# $2 : duration (in sec)
show_delayed_redirect() {
	printf "<!DOCTYPE html>"
    printf "<html>"
	print_html_head	\
		$(printf "	<meta http-equiv='refresh' content='%i; url=%s' />" "$2" "$1")
    printf "<body>"
	printf "<div class='main_div'>"
    printf "	<h1>Loading...</h1>"
	printf "	<img src='%s' alt='Loading...' class='loading_img' />" "$AWAIT_IMG_URI"
    printf "</div>"
	print_html_bottom_page_content
    printf "</body>"
    printf "</html>"
}

