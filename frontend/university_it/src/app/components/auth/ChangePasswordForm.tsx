"use client"

import { changePassword } from "@/app/services/auth/auth";
import { Button, Form, FormProps, Input } from "antd";
import { useState } from "react";

type Props = {
    email: string;
}

type FieldType = {
    oldPassword: string;
    newPassword: string;
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

const ChangePasswordForm = ({ email }: Props) => {
    const [changed, setChanged] = useState(false);

    const onFinish: FormProps<FieldType>['onFinish'] = async (values) => {
        const changePassRequest = {
            email: email,
            oldPassword: values.oldPassword,
            newPassword: values.newPassword
        };

        await changePassword(changePassRequest);

        setChanged(true);
    };

    return (
        changed ? (
        <Form
            name="success"
            style={{ maxWidth: 600 }}
        >
            <Form.Item>
                <h2>Password successfully changed!</h2>
                <a href="/profile">Return to profile page now!</a>
            </Form.Item>
        </Form>
        ) : (
        <Form
            {...formItemLayout}
            name="changePassword"
            style={{ maxWidth: 600 }}
            onFinish={onFinish}
        >
            <Form.Item
                name="oldPassword"
                label="Old password"
                hasFeedback 
                rules={[{ required: true, message: "Please input your old password!" }]}               
            >
                <Input.Password />
            </Form.Item>
            <Form.Item
                name="newPassword"
                label="New password"
                hasFeedback 
                rules={[{ required: true, message: "Please input your password!" }]}               
            >
                <Input.Password />
            </Form.Item>
            <Form.Item
                name="confirm"
                label="Confirm password"
                dependencies={['newPassword']}
                hasFeedback 
                rules={[
                    { required: true, message: "Please confirm your password!" },
                    ({ getFieldValue }) => ({
                        validator(_, value) {
                            if (!value || getFieldValue("newPassword") === value) {
                                return Promise.resolve();
                            };
                            return Promise.reject(new Error("The new password that you entered do not match!"));
                        }
                    })
                ]}               
            >
                <Input.Password />
            </Form.Item>
            <Form.Item wrapperCol={{ offset: 10 }}>
                <Button block type="primary" htmlType="submit">
                    Change password
                </Button>
                or <a href="/profile">Return to profile page</a>
            </Form.Item>
        </Form>)
    )
}

export { ChangePasswordForm };