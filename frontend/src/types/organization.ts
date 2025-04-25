export interface Organization {
    id: string;
    administratorId: string;
    name: string;
    description: string;
    imageLogoUrl: string;
}
export interface OrganizationShortInfo {
    name: string;
    description: string;
    imageLogoUrl: string;
}