FROM mcr.microsoft.com/vscode/devcontainers/dotnet:10.0@sha256:da953a31996cf89df8c506a5d7ed9374f4285af6f728b55d772cd2d8e205cdef
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install lts/* 2>&1"
RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g cspell@10.0.1" 2>&1
