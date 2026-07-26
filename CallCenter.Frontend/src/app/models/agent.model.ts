export interface Agent {
  id: number;
  fullName: string;
  email: string;
  extension: string;
  status: string;
  queueId?: number | null;
  createdAt: string;
  queueName?: string;
}