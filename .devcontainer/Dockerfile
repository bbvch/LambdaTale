FROM mcr.microsoft.com/vscode/devcontainers/dotnet:10.0@sha256:97961d9ef283e2404a79fb9bdca5837b6af57f5f8688556e21cbc5686af72770
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
