#!/bin/bash

protoc *.proto --csharp_out=./ServerClientClass
sudo cp -r ./ServerClientClass /mnt/f/git_repo/FlappyWorld/Assets/