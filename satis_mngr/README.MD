Requirement
===========
$> python3 -m pip install fastapi uvicorn[standard]
$> apt install python3-lxml


TODO
=====
LOGIN page


INSTALL NOTES
=============
see INSTALL.MD for instructions


DEBUG NOTES
=============
$> uvicorn main:app --reload


NOTES 
=============
In order to use "systemctl --user", XDG_RUNTIME_DIR must be set.
If not, use this workarround :
$> echo "export XDG_RUNTIME_DIR=/run/user/$(id -u)" >> ~/.bashrc
$> source ~/.bashrc
