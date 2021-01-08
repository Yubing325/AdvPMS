import { WorkItemState } from "./WorkItemState";

export interface WorkItem {
    id: string;
    title: string;
    description: string;
    created: Date;
    lastModified: Date;
    priority: number;
    state: WorkItemState;
    iterationId: string;
    iteration: string;
}


