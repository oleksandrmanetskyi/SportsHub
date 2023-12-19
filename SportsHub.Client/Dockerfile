FROM node:16 as builder

# install and cache app dependencies
COPY package*.json ./
RUN npm install && mkdir /sports_hub_client && mv ./node_modules ./sports_hub_client

WORKDIR /sports_hub_client

COPY . .

ENV NODE_OPTIONS=--max_old_space_size=4096
RUN npm run build


# ------------------------------------------------------
# Production Build
# ------------------------------------------------------
FROM nginx:alpine
COPY --from=builder /sports_hub_client/build /usr/share/nginx/html
RUN rm /etc/nginx/conf.d/default.conf
COPY nginx/nginx.conf /etc/nginx/conf.d
EXPOSE 80

ENTRYPOINT ["nginx", "-g", "daemon off;"]

