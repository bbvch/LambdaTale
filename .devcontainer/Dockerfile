FROM mcr.microsoft.com/vscode/devcontainers/dotnet:8.0@sha256:6f1322a6a0b6b236b851b28cd2234697f09911a282fa12cc04aeb880da4c1436
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
