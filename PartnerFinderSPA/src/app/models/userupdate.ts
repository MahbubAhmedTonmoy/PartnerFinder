export interface UserUpdate {
    username: string;
    knownAs: string;
    age: number;
    gender: string;
    created: Date;
    lastActive: Date;
    city: string;
    country: string;
    interests?: string;
    introduction?: string;
    lookingFor?: string;
}