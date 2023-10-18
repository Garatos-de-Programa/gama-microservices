import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    discardResponseBodies: true,
    scenarios: {
        occurrencesCreation: {
            executor: 'constant-vus',
            vus: 5,
            duration: '120s',
            exec: 'occurrencesCreationTest',
        },
        trafficFinesCreation: {
            executor: 'constant-vus',
            vus: 200,
            duration: '120s',
            exec: 'trafficFinesCreationTest',
        },
        trafficFineSearch: {
            executor: 'constant-vus',
            vus: 5,
            duration: '120s',
            exec: 'trafficFineSearchTest',
        },
    },
};

export function occurrencesCreationTest() {
    const baseUrl = 'http://ec2-3-145-176-128.us-east-2.compute.amazonaws.com';

    const headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ',
    };

    const requestBody = {
        latitude: -22.736491,
        longitude: -47.332322,
        location: 'Dom bosco',
        name: 'Teste com ibagens',
        description: 'ibagens',
        occurrenceStatusId: 1,
        occurrenceTypeId: 1,
        occurrenceUrgencyLevelId: 1,
        imageUrl: 'https://gama.bucket.com.br.s3.amazonaws.com/71d9b2dc-c103-4add-ba5d-7d07df18f588.jpg'
    };

    const response = http.post(`${baseUrl}/v1/occurrences`, JSON.stringify(requestBody), { headers });

    check(response, {
        'Status is 200 or 201': (r) => r.status === 200 || r.status === 201,
    });
}

export function trafficFinesCreationTest() {
    const baseUrl = 'http://ec2-3-145-176-128.us-east-2.compute.amazonaws.com';

    const headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ',
    };

    const requestBody = {
        licensePlate: 'nou-2337',
        latitude: 15.23456,
        longitude: -15.23456,
        trafficViolations: [
            {
                id: 1
            }
        ],
        imageUrl: 'https://gama.bucket.com.br.s3.amazonaws.com/35d4eca0-fd39-461f-a677-e826334024a1.jpg'
    };

    const response = http.post(`${baseUrl}/v1/traffic-fines`, JSON.stringify(requestBody), { headers });

    check(response, {
        'Status is 200 or 201': (r) => r.status === 200 || r.status === 201,
    });
}

export function trafficFineSearchTest() {
    const baseUrl = 'http://ec2-3-145-176-128.us-east-2.compute.amazonaws.com/v1';


    const headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ',
    };

    const queryParams = {
        CreatedSince: '2023-09-30T00:00:00Z',
        CreatedUntil: '2023-09-30T23:59:59Z',
    };

    const response = http.get(`${baseUrl}/traffic-fines`, queryParams, { headers });

    check(response, {
        'Status is 200': (r) => r.status === 200,
    });

    sleep(1);
}

