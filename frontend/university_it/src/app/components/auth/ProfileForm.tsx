"use client"

import { Button, Form, FormProps, Space } from "antd";
import { useEffect, useState } from "react";
import { getUserByEmail, updateUser } from "@/app/services/auth/users";
import Input from "antd/es/input/Input";

type Props = {
    email: string;
}

type FieldType = {
    email: string;
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

const ProfileForm = ({ email }: Props) => {
    const [profile, setProfile] = useState<User>();
    const [loading, setLoading] = useState(true);
    const [editMode, setEditMode] = useState(false);
    
    useEffect(() => {
        const getProfile = async () => {
            const user = await getUserByEmail(email);
            setProfile(user);
            setLoading(false);

            form.setFieldsValue({
                username: user.userName,
                fullname: user.fullName,
                position: user.position,
                phonenumber: user.phoneNumber
            });
        };
        
        getProfile();
    }, []);

    const [form] = Form.useForm();
    
    const onFinish: FormProps<FieldType>['onFinish'] = async (values) => {
        console.log('Success:', values);
        const userRequest = {
            email: email,
            userName: values.username,
            fullName: values.fullname,
            position: values.position,
            phoneNumber: values.phonenumber
        };

        if (profile?.id) {
            await updateUser(profile.id, userRequest);

            const user = await getUserByEmail(email);
            setProfile(user);
        };
        setEditMode(false);
    };

    const onEditModeChange = () => {
        setEditMode(!editMode);
    };

    return (
        <Form
            {...formItemLayout}
            name="profile"
            form={form}
            style={{ maxWidth: 600 }}
            onFinish={onFinish}
        >
            <Form.Item
                name="email"
                label="E-mail"
            >
                <p>{email}</p>
            </Form.Item>
            <Form.Item
                name="username"
                label="User name"
                rules={[{ required: true, message: "Please input your username!" }]}
            >
                {!editMode ? (
                    <p>{profile?.userName}</p>
                ) : (
                    <Input />
                )}
            </Form.Item>
            <Form.Item
                name="fullname"
                label="Full name"
                rules={[{ whitespace: true }]}               
            >
                {!editMode ? (
                    <p>{profile?.fullName}</p>
                ) : (
                    <Input />
                )}
            </Form.Item>
            <Form.Item
                name="position"
                label="Position"
                rules={[{ whitespace: true }]}               
            >
                {!editMode ? (
                    <p>{profile?.position}</p>
                ) : (
                    <Input />
                )}
            </Form.Item>
            <Form.Item
                name="phonenumber"
                label="Phone number"
                rules={[{ whitespace: true }]}               
            >
                {!editMode ? (
                    <p>{profile?.phoneNumber}</p>
                ) : (
                    <Input />
                )}
            </Form.Item>
            <Form.Item>
                <Space>
                    <Button type="primary" htmlType="submit" disabled={!editMode}>
                        Save
                    </Button>
                    <Button htmlType="button" onClick={onEditModeChange}>
                        {editMode ? ("View") : ("Edit")}
                    </Button>
                    <Button type="link" htmlType="button" href="/profile/change_pass">
                        Change password
                    </Button>
                </Space>
            </Form.Item>
        </Form>
    )
}

export { ProfileForm };