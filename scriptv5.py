import torch
import cv2
import matplotlib.pyplot as plt

# Load a pretrained YOLOv5 model
model = torch.hub.load('ultralytics/yolov5', 'custom', path='Assets/yolov5/best.pt')

# Perform object detection on an image using the model
image_path = 'C:/Users/opria/Pictures/220df0d70_jpg.rf.1bf3171e65d6c1a59d99e58d5893b79b.jpg'
results = model(image_path)

# Convert results to a format suitable for drawing bounding boxes
image = cv2.imread(image_path)
results = results.pandas().xyxy[0]  # Get the results in pandas dataframe format

# Open the file to write coordinates
with open('coord.txt', 'w') as f:
    for _, row in results.iterrows():
        x1, y1, x2, y2 = int(row['xmin']), int(row['ymin']), int(row['xmax']), int(row['ymax'])
        confidence = row['confidence']
        class_name = row['name']

        f.write(f'{class_name} {confidence:.2f} {x1} {y1} {x2} {y2}\n')

print(f'Coordinates saved to coord.txt')