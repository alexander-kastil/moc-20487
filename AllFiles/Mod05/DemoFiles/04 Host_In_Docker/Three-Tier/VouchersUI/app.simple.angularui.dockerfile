FROM node:10.5 as node
LABEL author="Alexander Pajer"
WORKDIR /app

RUN npm i @angular/cli

# copy package.json
COPY package.json package.json
# install packages
RUN npm install
COPY . .
# create production build
RUN npm run build -- --prod