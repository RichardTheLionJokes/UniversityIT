"use client"

import { register } from "@/app/services/auth/auth";
import { Button, Form, FormProps, Input } from "antd";
import { useState } from "react";

type FieldType = {
    email: string;
    password: string;
    username: string;
    fullname?: string;
    position?: string;
    phonenumber?: string;
}

const formItemLayout = {
    labelCol: {
      xs: { span: 24 },
      sm: { span: 10 },
    },
    wrapperCol: {
      xs: { span: 24 },
      sm: { span: 16 },
    },
  };

const RegForm = () => {
    const [registered, setRegistered] = useState(false);
    const [email, setEmail] = useState("");

    const onFinish: FormProps<FieldType>['onFinish'] = async (values) => {
        const userRequest = {
            email: values.email,
            password: values.password,
            userName: values.username,
            fullName: values.fullname,
            position: values.position,
            phoneNumber: values.phonenumber
        };

        await register(userRequest);

        setRegistered(true);
    };

    return (
        registered ? (
        <Form
            {...formItemLayout}
            name="success"
            style={{ maxWidth: 600 }}
        >
            <Form.Item>
                <h2>User {email} registered successfully!</h2>
                <a href="/signin">Sign in now!</a>
            </Form.Item>
        </Form>
        ) : (
        <Form
            {...formItemLayout}
            name="register"
            style={{ maxWidth: 600 }}
            onFinish={onFinish}
        >
            <Form.Item
                name="email"
                label="E-mail"                
                rules={[
                    { type: "email", message: "The input is not valid E-mail!" },
                    { required: true, message: "Please input your email!" }
                ]}
            >
                <Input onChange={(event) => setEmail(event.target.value)} />
            </Form.Item>
            <Form.Item
                name="username"
                label="User name"
                rules={[{ required: true, message: "Please input your username!" }]}               
            >
                <Input />
            </Form.Item>
            <Form.Item
                name="fullname"
                label="Full name"
                rules={[{ whitespace: true }]}               
            >
                <Input />
            </Form.Item>
            <Form.Item
                name="position"
                label="Position"
                rules={[{ whitespace: true }]}               
            >
                <Input />
            </Form.Item>
            <Form.Item
                name="phonenumber"
                label="Phone number"
                rules={[{ whitespace: true }]}               
            >
                <Input />
            </Form.Item>
            <Form.Item wrapperCol={{ offset: 10 }}>
                <Button block type="primary" htmlType="submit">
                    Register
                </Button>
                or <a href="/signin">Sign in now!</a>
            </Form.Item>
        </Form>)
    )
}

export { RegForm };