import { WorkItemState } from "./WorkItemState";

export interface AddWorkItemDto {
    title: string;
    description: string;
    created: Date;
    priority: number;
    state: WorkItemState;
}
