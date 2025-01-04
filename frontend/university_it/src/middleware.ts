export { default } from "next-auth/middleware"

export const config = { matcher: ["/profile", "/servers", "/helpdesk", "/documents"] }
//export const config = { matcher: ["/serv_mon", "/protected/:path*"] }