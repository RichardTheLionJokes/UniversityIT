import Layout, { Content, Footer, Header } from "antd/es/layout/layout";
import "./globals.css";
import { Providers } from "./components/auth/Providers";
import { Navigation } from "./components/common/Navigation";

const menuLinks = [
  {key: "home", label: "Home", href: "/"},
  {key: "servers", label: "Servers", href: "/serv_mon"}
]

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <Providers>
        <Layout style={{ minHeight: "100vh" }}>
          <Header>
            <Navigation navLinks={menuLinks}></Navigation>
          </Header>
          <Content style={{ padding: "0 48px" }}>{children}</Content>
          <Footer style={{ textAlign: "center" }}>
            University_IT 2024 Created by Sergey Sklyar
          </Footer>
        </Layout>
        </Providers>
      </body>
    </html>
  );
}