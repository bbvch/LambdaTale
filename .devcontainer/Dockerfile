FROM mcr.microsoft.com/vscode/devcontainers/dotnet:8.0@sha256:084cb533c106b5aefee663ab4c884de3bf3541f2b6ea21345c9843b4de95700d
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
