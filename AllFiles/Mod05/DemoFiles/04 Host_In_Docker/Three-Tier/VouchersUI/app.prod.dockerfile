##### Stage 1 - Create the build-image 
FROM node:10.5 as node
LABEL author="Alexander Pajer"
WORKDIR /app
COPY package.json package.json
RUN npm install
COPY . .
RUN npm run build -- --prod

##### Stage 2 - Create the run-time-image 
FROM nginx:alpine
VOLUME /var/cache/nginx

# Take from node-build
COPY --from=node /app/dist/VouchersUI /usr/share/nginx/html
# Take from proj folder
COPY ./config/nginx.conf /etc/nginx/conf.d/default.conf
