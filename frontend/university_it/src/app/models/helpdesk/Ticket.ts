interface Ticket {
    id: string;
    name: string;
    description: string;
    place: string;
    createdAt: Date;
    notificationsSent: boolean;
    isCompleted: boolean;
    authorId: string;
    author: string;
}