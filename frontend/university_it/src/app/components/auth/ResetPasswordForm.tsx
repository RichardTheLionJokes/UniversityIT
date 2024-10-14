"use client"

import { resetPassword } from "@/app/services/auth/auth";
import { MailOutlined } from '@ant-design/icons';
import { Button, Form, FormProps, Input } from "antd";
import { useState } from "react";

type FieldType = {
    email: string;
}

const ResetPasswordForm = () => {
    const [sent, setSent] = useState(false);
    const [email, setEmail] = useState("");

    const onFinish: FormProps<FieldType>['onFinish'] = async (values) => {
        await resetPassword(values.email);

        setSent(true);
    };

    return (
        sent ? (
        <Form
            name="success"
            style={{ maxWidth: 600 }}
        >
            <Form.Item>
                <h2>Password sent to mailbox {email}!</h2>
                <a href="/signin">Sign in now!</a>
            </Form.Item>
        </Form>
        ) : (
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
                <Input prefix={<MailOutlined />} placeholder="Email" onChange={(event) => setEmail(event.target.value)} />
            </Form.Item>
            <Form.Item>
                <Button block type="primary" htmlType="submit">
                    Send new password
                </Button>
                or <a href="/signin">Sign in now!</a>
            </Form.Item>
        </Form>)
    )
}

export { ResetPasswordForm };