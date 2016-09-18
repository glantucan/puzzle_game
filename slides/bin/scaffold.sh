#!/bin/bash
# Scaffolding
mkdir ../client
mkdir ../client/css
mkdir ../client/css/theme

mkdir ../server
mkdir ../server/css
mkdir ../server/css/theme

cp -r ./plugin ../client/
cp -r ./plugin ../server/

cp -r ./lib ../client/
cp -r ./lib ../server/

# Style
cp ./css/theme/black.css ../client/css/theme/
cp ./css/*.css ../client/css/
cp -r ./css/imgs ../client/css/

cp ./css/theme/black.css ../server/css/theme/
cp ./css/*.css ../server/css/
cp -r ./css/imgs ../server/css/

# Content
cp ./t*_client.html ../client/
cp ./*.md ../client/
cp -r ./imgs ../client/

cp ./t*_server.html ../server/
cp ./*.md ../server/
mkdir ../server/imgs
cp -r ./imgs ../server/

cp ./t00_index.html ../index.html

# Create a printable copy of the slides.
#	Will have to print throug the gihub page removing some parts using the chrome debugger, but this will do for now
grunt remove_Html_Comments
cp ./git_slides/*.md ../printableSlides/

# small mongoose server, just in case
cp ./mongoose.* ../client/
cp ./mongoose.* ../server/
