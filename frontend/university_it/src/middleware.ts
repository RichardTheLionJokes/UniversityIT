export { default } from "next-auth/middleware"

export const config = { matcher: ["/profile", "/servers"] }
//export const config = { matcher: ["/serv_mon", "/protected/:path*"] }