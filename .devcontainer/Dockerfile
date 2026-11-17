FROM mcr.microsoft.com/vscode/devcontainers/dotnet:11.0@sha256:375b52bd26d117597baff3d65def1e03d05b9725a22215d132bb92d44d2639be
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell@10.5.1" 2>&1
