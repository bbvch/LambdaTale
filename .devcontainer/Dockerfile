FROM mcr.microsoft.com/vscode/devcontainers/dotnet:9.0@sha256:89968a8be907bcff432981f461457abaa9c40fbacff05128d30b8ea04aa0a127
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
