export interface WorkItem {
    id: string;
    title: string;
    description: string;
    created: Date;
    lastModified: Date;
    priority: number;
    state: WorkItemState;
    iterationId: string;
}


export enum WorkItemState
{
        New = 0,
        Active = 1,
        Resolved=2,
        Closed=3
}