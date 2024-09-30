export { default } from "next-auth/middleware"

export const config = { matcher: ["/profile", "/serv_mon"] }
//export const config = { matcher: ["/serv_mon", "/protected/:path*"] }