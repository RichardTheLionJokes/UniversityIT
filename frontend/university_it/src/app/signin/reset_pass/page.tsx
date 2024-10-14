import { ResetPasswordForm } from "@/app/components/auth/ResetPasswordForm";

export default async function Register() {
    return (
        <div className="stack">
            <h1>Reset password</h1>
            <ResetPasswordForm />
        </div>
    )
}