import { RegForm } from "@/app/components/auth/RegForm";

export default async function Register() {
    return (
        <div className="stack">
            <h1>Registration</h1>
            <RegForm />
        </div>
    )
}