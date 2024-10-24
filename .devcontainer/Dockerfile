FROM mcr.microsoft.com/vscode/devcontainers/dotnet:9.0@sha256:7983258e07166b4a698d0c59375cb93ea77b04bc7fbfc1d4bf8f05cef3be9a7a
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
