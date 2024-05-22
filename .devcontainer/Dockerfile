FROM mcr.microsoft.com/vscode/devcontainers/dotnet:8.0@sha256:4ba060c075168b454bb48ae506a9cf86cab56c015ddfcab4cae72b9a0c30fa31
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
