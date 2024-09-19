#!/bin/bash

tmux new-session -d -s my_session

tmux send-keys -t my_session 'dotnet watch' C-m

tmux split-window -h

tmux send-keys -t my_session:0.1 'npx tailwindcss -i ./Styles/tailwind.css -o ./wwwroot/tailwind.css --watch' C-m

tmux attach -t my_session

