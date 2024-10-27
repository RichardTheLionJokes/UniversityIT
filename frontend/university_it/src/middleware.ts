export { default } from "next-auth/middleware"

export const config = { matcher: ["/profile", "/servers", "/helpdesk"] }
//export const config = { matcher: ["/serv_mon", "/protected/:path*"] }