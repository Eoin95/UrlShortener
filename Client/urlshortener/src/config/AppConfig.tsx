import config from './config.json';

export class AppConfig {
    private constructor(){}

    public static getServerURL(): string {
        return config.SERVER_URL as string;
    }

    public static getClientURL(): string {
        return config.CLIENT_URL as string;
    }
}