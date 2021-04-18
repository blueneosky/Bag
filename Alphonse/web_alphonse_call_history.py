#!/usr/bin/env python3
import json
from datetime import datetime
from dateutil import tz

from web_alphonse_common import *

print(get_html_header())
print("<!DOCTYPE html>")
print("<html>")
print("  <head>")
print("    <meta charset=\"utf-8\" />")
print("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">")
print("    <link rel=\"stylesheet\" href=\"/alphonse_style.css\" />")
print("    <title>Alphonse - Call history</title>")
print("  </head>")
print("  <body>")
if is_local_call():
    print("    <div class=\"navigation_area\">")
    print("      <ul>")
    print("        <li><a href=\"./whitelist.html\">White list</a></li>")
    print("        <li><a href=\"./blacklist.html\">Black list</a></li>")
    print("        <li><a href=\"./phone_book.html\">Phone book</a></li>")
    print("      </ul>")
    print("    </div>")
print("    <h1>Call history</h1>")
print("")
print("    <div class=\"main_array\">")
print("      <table>")
print("        <thead>")
print("          <tr>")
print("            <th>Timestamp</th>")
print("            <th>Who</th>")
print("            <th>Actions</th>")
print("          </tr>")
print("        </thead>")
print("        <tbody>")

conf = get_alphonse_config().load()
with open(conf.call_history_file, 'r') as file:
    lines = file.readlines()
lines.reverse()
for line in lines:
    line = line.translate({ord(i): None for i in '\r\n'})
    if len(line) == 0:
        continue  # empty line
    if line.startswith("#"):
        continue  # commented line

    history = json.loads(line)
    timestamp = datetime.fromisoformat(history['timestamp'])
    number = history['number']
    print(f"<tr><td>{timestamp}</td><td>{number}</td><td>[/]</td></tr>")

print("        </tbody>")
print("      </table>")
print("    </div>")
print("  </body>")
print("</html>")
print("")
