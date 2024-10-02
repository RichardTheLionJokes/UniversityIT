import Layout, { Content, Footer, Header } from "antd/es/layout/layout";
import "./globals.css";
import { Providers } from "./components/auth/Providers";
import { Navigation } from "./components/common/Navigation";
import Link from "next/link";

const menuLinks = [
  {key: "home", label: <Link href="/">Home</Link>},           
];

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