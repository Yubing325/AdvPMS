import { WorkItemState } from "./workItem";

export interface AddWorkItemDto {
    title: string;
    description: string;
    created: Date;
    priority: number;
    state: WorkItemState;
}
