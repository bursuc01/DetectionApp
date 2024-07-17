import torch
import cv2
import matplotlib.pyplot as plt

# Load a pretrained YOLOv5 model
model = torch.hub.load('ultralytics/yolov5', 'custom', path='Assets/yolov5/best.pt')

# Perform object detection on an image using the model
image_path = 'C:/Users/opria/OneDrive/Documents/Licenta/DetectionApp/Assets/source.jpg'
results = model(image_path)

# Convert results to a format suitable for drawing bounding boxes
image = cv2.imread(image_path)
results = results.pandas().xyxy[0]  # Get the results in pandas dataframe format

for _, row in results.iterrows():
    x1, y1, x2, y2 = int(row['xmin']), int(row['ymin']), int(row['xmax']), int(row['ymax'])
    confidence = row['confidence']
    class_name = row['name']
    print(f'ship {confidence:.2f} {x1} {y1} {x2} {y2}')
