FROM mcr.microsoft.com/vscode/devcontainers/dotnet:9.0@sha256:cfaf00cc2d47e5aa602b67a20aaacefcc564b21cb3812a58ecdcb13cfce167af
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell" 2>&1
