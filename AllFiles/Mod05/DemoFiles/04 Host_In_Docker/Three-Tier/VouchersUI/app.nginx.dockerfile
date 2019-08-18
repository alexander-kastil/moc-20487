FROM nginx:alpine
LABEL author="Alexander Pajer"
#Copies nginx.conf file
COPY ./config/nginx.conf /etc/nginx/conf.d/default.conf
