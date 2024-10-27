"use client"

import { signIn } from "next-auth/react";
import { useRouter, useSearchParams } from "next/navigation";
import { login } from "@/app/services/auth/auth";
import { LockOutlined, MailOutlined } from '@ant-design/icons';
import { Button, Form, FormProps, Input } from "antd";

type FieldType = {
    email: string;
    password: string;
}

const SignInForm = () => {
    const router = useRouter();

    const searchParams = useSearchParams();
    const callbackUrl = searchParams?.get("callbackUrl") || "/profile";

    const onFinish: FormProps<FieldType>['onFinish'] = async (values) => {
        const user = await login(values.email, values.password);
        localStorage.setItem("userId", user.id);

        const res = await signIn("credentials", {
            email: user.email,
            name: user.name,
            redirect: false
        });

        if (res && !res.error) {
            router.push(callbackUrl);
        } else {
            console.log(res);
        }
    };

    return (
        <Form
            name="login"
            style={{ maxWidth: 600 }}
            onFinish={onFinish}
        >
            <Form.Item
                name="email"
                rules={[
                    { type: "email", message: "The input is not valid E-mail!" },
                    { required: true, message: "Please input your email!" }
                ]}
            >
                <Input prefix={<MailOutlined />} placeholder="Email" />
            </Form.Item>
            <Form.Item
                name="password"
                rules={[{ required: true, message: "Please input your password!" }]}
            >
                <Input.Password prefix={<LockOutlined />} placeholder="Password" />
            </Form.Item>
            <Form.Item>
                <Button block type="primary" htmlType="submit">
                    Sign in
                </Button>
                or <a href="/signin/register">Register now!</a>
            </Form.Item>
            <Form.Item>
                <a href="/signin/reset_pass">Forgot your password?</a>
            </Form.Item>
        </Form>
    )
}

export { SignInForm };