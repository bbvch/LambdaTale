FROM mcr.microsoft.com/vscode/devcontainers/dotnet:9.0@sha256:d99e4e4a3de2bf9249b985e7cb05024d9227361bd346611c0b7f352360c4dc3b
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
